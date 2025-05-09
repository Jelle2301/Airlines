using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Airlines.Extensions;
using Airlines.ViewModels;
using AutoMapper;
using Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;
using SQLitePCL;
using Util.Mail;
using Util.Mail.interfaces;
using Util.PDF;
using Util.PDF.interfaces;

namespace Airlines.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IZitplaatsService _zitplaatsService;
        private readonly ITicketService _ticketService;
        private readonly IBoekingService _boekingService;
        private readonly IEmailSend _emailSend;
        private readonly ICreatePDF _createPDF;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ShoppingCartController(IMapper mapper, IZitplaatsService zitplaatsService, ITicketService ticketService, IBoekingService boekingService, IEmailSend emailSend, ICreatePDF createPDF, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _zitplaatsService = zitplaatsService;
            _ticketService = ticketService;
            _boekingService = boekingService;
            _emailSend = emailSend;
            _createPDF = createPDF;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

                foreach (CartVM cartItem in cartList.Carts)
                {
                    var seat = await _zitplaatsService.GetAllBeschikbareZitplaatsenByVluchtAsync(cartItem.Ticket.Vlucht.VluchtId);
                    if (seat != null)
                    {
                        cartItem.Ticket.Zitplaats = _mapper.Map<ZitplaatsVM>(seat);
                        cartItem.Ticket.Zitplaats.ZitplaatsId = cartItem.Ticket.Zitplaats.ZitplaatsId;
                        await _zitplaatsService.MaakZitplaatsBezet(cartItem.Ticket.Zitplaats.ZitplaatsId);
                    }
                }
                HttpContext.Session.SetObject("ShoppingCart", cartList);
                return View(cartList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostData()
        {
            try
            {
                ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
                List <Boeking> bookings = new List<Boeking>();

                if (cartList == null)
                {
                    return RedirectToAction("ShoppingCartLeegIndex", "BoekingAfronding");
                }
                if (cartList.Carts == null)
                {
                    return RedirectToAction("ShoppingCartLeegIndex", "BoekingAfronding");
                }
                foreach (CartVM cartItem in cartList.Carts)
                {
                    var ticketEntity = _mapper.Map<Ticket>(cartItem.Ticket);
                    await _ticketService.AddAsync(ticketEntity);

                    var boekingVM = new BoekingVM();
                    boekingVM.TicketId = ticketEntity.TicketId;
                    boekingVM.DatumBoeking = DateOnly.FromDateTime(DateTime.Now);
                    boekingVM.Status = "Voltooid";
                    boekingVM.TotalePrijs = ticketEntity.Prijs;
                    boekingVM.VoornaamBoeking = ticketEntity.Voornaam;
                    boekingVM.AchternaamBoeking = ticketEntity.Achternaam;
                    boekingVM.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    var boekingEntity = _mapper.Map<Boeking>(boekingVM);
                    await _boekingService.AddAsync(boekingEntity);
                    bookings.Add(boekingEntity);
                }

                // Mail sturen met geboekte tickets die binnen komen als pdf
                generateMailAndSendIt(bookings);

                HttpContext.Session.Remove("ShoppingCart");

                return RedirectToAction("Index", "BoekingAfronding");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return RedirectToAction("ShoppingCartLeegIndex", "BoekingAfronding");
        }
        public IActionResult Delete(int plaatsInShoppingCart)
        {
            try
            {
                ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");//hoofdlettergevoelig
                CartVM? itemToRemove = cartList?.Carts?.ElementAt(plaatsInShoppingCart);
                cartList?.Carts?.RemoveAt(plaatsInShoppingCart);

                if (itemToRemove != null)
                {
                    HttpContext.Session.Remove("ShoppingCart");
                    HttpContext.Session.SetObject("ShoppingCart", cartList);
                }
                return View("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Er is een fout opgetreden");
            }
            return View("index");

        }

        public Task generateMailAndSendIt(List<Boeking> bookings)
        {
            var logoPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "Geluwe airlines blauw.jpg");
            List<MemoryStream> pdfStreams = new List<MemoryStream>();
            List<string> attachmentNames = new List<string>();
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var mailSubject = "Tickets Gilwe Airlines";

            StringBuilder mailBody = new StringBuilder();
            mailBody.Append($"Best {User.FindFirstValue(ClaimTypes.Name)}");
            mailBody.Append("Hierbij ontvangt u uw tickets die u reeds heeft besteld.");
            mailBody.Append("Hartelijk dank om via Gilwe Airlines te vliegen.");
            mailBody.Append("U vind uw tickets in de bijlage.");
            mailBody.Append("Met vriendelijke groeten");
            mailBody.Append("Gilwe Airlines");

            var bodyMail = mailBody.ToString();

            foreach (var booking in bookings)
            {
                    var pdfStream = _createPDF.CreatePDFDocumentAsync(logoPath, booking, booking.Ticket.Vlucht);
                    pdfStreams.Add(pdfStream);

                    var attachmentName = $"Ticket_{booking.TicketId}.pdf";
                    attachmentNames.Add(attachmentName);
            }

            return _emailSend.SendEmailWithPDFSAsync(userEmail, mailSubject, bodyMail, pdfStreams, attachmentNames);
        }
    }
}
