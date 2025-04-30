const TicketModule = (() => {
    let totaalPrijsElement;
    let totaalPrijsElementFormulier;
    let originelePrijs = 0;
    let huidigeMaaltijdPrijs = 0;
    let huidigeReisklassePrijs = 0;

    const init = () => {
        totaalPrijsElement = document.getElementById("totaalPrijs");
        totaalPrijsElementFormulier = document.getElementsByName("Prijs");

        originelePrijs = parseFloat(totaalPrijsElement.textContent);

        // Voeg event listeners toe aan de maaltijdkaarten
        document.querySelectorAll(".maaltijdKaart").forEach(kaart => {
            kaart.addEventListener("click", () => selecteerMaaltijd(kaart));
        });

        // Voeg event listeners toe aan de reisklasse-radio's
        document.querySelectorAll("input[name='reisklasse']").forEach(radio => {
            radio.addEventListener("change", () => selecteerReisklasse(radio));
        });
    };

    const selecteerMaaltijd = (element) => {
        document.querySelectorAll(".maaltijdKaart").forEach(x => x.classList.remove("selected"));
        element.classList.add("selected");

        let nieuweMaaltijdPrijs = parseFloat(element.getAttribute("data-prijs")) || 0;
        originelePrijs = originelePrijs - huidigeMaaltijdPrijs + nieuweMaaltijdPrijs;
        huidigeMaaltijdPrijs = nieuweMaaltijdPrijs;

        let maaltijdId = element.id.replace("Maaltijd", ""); 
        document.querySelector("input[name='MaaltijdId']").value = maaltijdId;

        updateTotaalPrijs();
    };

    const selecteerReisklasse = (element) => {
        let nieuweReisklassePrijs = parseFloat(element.getAttribute("data-prijs")) || 0;
        originelePrijs = originelePrijs - huidigeReisklassePrijs + nieuweReisklassePrijs;
        huidigeReisklassePrijs = nieuweReisklassePrijs;

        let reisklasseId = element.id.replace("Reisklasse", ""); // bijv. Reisklasse2 → 2
        document.querySelector("input[name='ReisklasseId']").value = reisklasseId;

        updateTotaalPrijs();
    };

    const updateTotaalPrijs = () => {
        totaalPrijsElement.textContent = originelePrijs.toFixed(2);
        totaalPrijsElementFormulier[0].value = originelePrijs;
    };

    return { init };
})();

document.addEventListener("DOMContentLoaded", TicketModule.init);