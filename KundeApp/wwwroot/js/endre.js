$(function () {
    // hent kunden med kunde-id fra url og vis denne i skjemaet

    const id = window.location.search.substring(1);
    const url = "Kunde/HentEn?" + id;
    $.get(url, function (kunde) {
        $("#id").val(kunde.id); // må ha med id (som er gjemt i html) inn i skjemaet
        $("#fornavn").val(kunde.fornavn);
        $("#etternavn").val(kunde.etternavn);
        $("#adresse").val(kunde.adresse);
        $("#postnr").val(kunde.postnr);
        $("#poststed").val(kunde.poststed);
    });
});

function endreKunde() {
    const kunde = {
        id: $("#id").val(), // må ha dene for å vite hvilken kunde som skal endres
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        adresse: $("#adresse").val(),
        postnr: $("#postnr").val(),
        poststed: $("#poststed").val()
    }
    $.post("Kunde/Endre", kunde, function (OK) {
        if (OK) {
            window.location.href = 'index.html';
        }
        else {
            $("#feil").html("Feil i db - prøv igjen senere");
        }
    });
}