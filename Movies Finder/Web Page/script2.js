const btnValider = document.querySelector('.btn-valider');

btnValider.addEventListener("click", async function (event) {
    event.preventDefault();

    const Titre = document.getElementById("titre").value.trim();
    const Genre = document.getElementById("genre").value.trim();
    const Acteur = document.getElementById("acteur_principal").value.trim();
    const DureeHtml = document.getElementById("duree").value;

    const Duree = DureeHtml ? DureeHtml : 300;

    const film = {
        title: Titre || "",
        lead_actor: Acteur || "",
        genre: Genre || "",
        duration_minutes: Duree,
    };

    // Référence à l'élément HTML pour afficher le message
    const responseMessage = document.getElementById("responseMessage");

    try {
        const response = await fetch("http://localhost:5046/api/AddFilmAPI", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(film),
        });

        if (!response.ok) {
            const errorText = await response.text();
            console.error("Erreur lors de l'appel à l'API :", errorText);
            responseMessage.textContent = "Erreur lors de l'ajout du film.";
            responseMessage.style.color = "red";
            return;
        }

        const result = await response.json(); // Supposons que l'API renvoie un JSON contenant true ou false
        if (result) {
            responseMessage.textContent = "Le film a été ajouté avec succès !";
            responseMessage.style.color = "green";
        } else {
            responseMessage.textContent = "Le film n'a pas pu être ajouté.";
            responseMessage.style.color = "red";
        }
    } catch (error) {
        console.error("Erreur réseau ou problème inattendu :", error);
        responseMessage.textContent = "Une erreur est survenue. Veuillez réessayer.";
        responseMessage.style.color = "red";
    }
});
