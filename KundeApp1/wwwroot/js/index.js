$(function (){
    hentAlleKunder();
});

function.hentAlleKunder() {
    $.get("kunde/hentAlle", function (kunder)) {
        formaterKunder(kunder);
    });
}

function formaterKudner(kunder) {
    let ut = "<table class='table table-striped'>" +
        "<tr>" +
    "<th>Navn</th><th>Adresse</th><th></th><th></th>" +
    "</tr>";
    for (let kunde of kunder) {
        ut += "<tr>" +
        "<td>" + kunde.navn + "</td>" + 
        "<td>" + kunde.adresse + "</td>" + 
        "</tr>";
    }
    ut += "</table>";
    $("#kundene").html(ut);
}