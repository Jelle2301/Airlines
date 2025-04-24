


//is een soort van module, die module wordt ingelaadt door die eventlistener van domcontent loeaded, wou dit eens proberen maar is ene nadeel, je moet cash refreshen voor nieuw scriptje, is niet echt probleem voor client maar kan eventueel mooier
const TicketModule = (() => {
    let totaalPrijsElement;
    let originelePrijs = 0;
    let huidigeMaaltijdPrijs = 0;
    let huidigeReisklassePrijs = 0;

      const init = () =>{
        totaalPrijsElement = document.getElementById("totaalPrijs");
        originelePrijs = parseFloat(totaalPrijsElement.textContent);

        document.querySelectorAll(".maaltijdKaart").forEach(kaart => {
            kaart.addEventListener("click", () => selecteerMaaltijd(kaart));
        });

        document.querySelectorAll("input[name='reisklasse']").forEach(radio => {
            radio.addEventListener("change", () => selecteerReisklasse(radio));
        });

    }

    const selecteerMaaltijd = (element) => {
        document.querySelectorAll(".maaltijdKaart").forEach(x => x.classList.remove("selected"));
        element.classList.add("selected");

        let nieuweMaaltijdPrijs = parseFloat(element.getAttribute("data-prijs")) || 0;
        originelePrijs = originelePrijs - huidigeMaaltijdPrijs + nieuweMaaltijdPrijs;
        huidigeMaaltijdPrijs = nieuweMaaltijdPrijs;

        updateTotaalPrijs();
    }

    const selecteerReisklasse = (element) => {
        let nieuweReisklassePrijs = parseFloat(element.getAttribute("data-prijs")) || 0;
        originelePrijs = originelePrijs - huidigeReisklassePrijs + nieuweReisklassePrijs;
        huidigeReisklassePrijs = nieuweReisklassePrijs;

        updateTotaalPrijs();
    }

    const updateTotaalPrijs = () => {
        totaalPrijsElement.textContent = originelePrijs.toFixed(2);
    }

    return { init };
})();

document.addEventListener("DOMContentLoaded", TicketModule.init);


