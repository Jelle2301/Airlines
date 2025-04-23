function selectMeal(card) {
    document.querySelectorAll('.maaltijdKaart').forEach(el => el.classList.remove('selected'));
    card.classList.add('selected');
}
