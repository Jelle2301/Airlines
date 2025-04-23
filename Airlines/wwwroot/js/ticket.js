

//is iets speciaals dat ik heb gevonden online, als hij scriptje laadt dan zal hij deze module direct laden en die init funcite uitvoeren, 
kon ook via een eventlistener met DOMContentLoaded maar wou eens iets neuws proberen
const TicketModule = (() => {
    let totaalPrijsElement;
    let originelePrijs = 0;
    let huidigeMaaltijdPrijs = 0;
    let huidigeReisklassePrijs = 0;

     init = () =>{
        totaalPrijsElement = document.getElementById("totaalPrijs");
        originelePrijs = parseFloat(totaalPrijsElement.textContent);

        document.querySelectorAll(".maaltijdKaart").forEach(kaart => {
            kaart.addEventListener("click", () => selecteerMaaltijd(kaart));
        });

        document.querySelectorAll("input[name='reisklasse']").forEach(radio => {
            radio.addEventListener("change", () => selecteerReisklasse(radio));
        });
    }

     selecteerMaaltijd = (element) => {
        document.querySelectorAll(".maaltijdKaart").forEach(x => x.classList.remove("selected"));
        element.classList.add("selected");

        let nieuweMaaltijdPrijs = parseFloat(element.getAttribute("data-prijs")) || 0;
        originelePrijs = originelePrijs - huidigeMaaltijdPrijs + nieuweMaaltijdPrijs;
        huidigeMaaltijdPrijs = nieuweMaaltijdPrijs;

        updateTotaalPrijs();
    }

     selecteerReisklasse = (element) => {
        let nieuweReisklassePrijs = parseFloat(element.getAttribute("data-prijs")) || 0;
        originelePrijs = originelePrijs - huidigeReisklassePrijs + nieuweReisklassePrijs;
        huidigeReisklassePrijs = nieuweReisklassePrijs;

        updateTotaalPrijs();
    }

     updateTotaalPrijs = () => {
        totaalPrijsElement.textContent = originelePrijs.toFixed(2);
    }

    return { init };
})();

document.addEventListener("DOMContentLoaded", TicketModule.init);


