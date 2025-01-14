const btnValider = document.querySelector('.btn-valider');

btnValider.addEventListener("click", async function (event) {
    event.preventDefault();

    const Titre = document.getElementById("titre").value;
    const Genre = document.getElementById("genre").value;
    const Acteur = document.getElementById("acteur_principal").value;
    const DureeHtml = document.getElementById("duree").value;

    console.log("Titre :", Titre);

    // Garder la durée directement en string (comme Swagger)
    const Duree = DureeHtml ? DureeHtml : 300;

    const film = {
        title: Titre || "",
        lead_actor: Acteur || "",
        genre: Genre || "",
        duration_minutes: Duree // Envoyé comme string
    };

    console.log("Durée sélectionnée :", Duree);
    console.log("Objet film envoyé :", film);

    try {
        const response = await fetch("http://localhost:5046/api/FilmAPI", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(film),
        });

        if (!response.ok) {
            console.error("Erreur lors de l'appel à l'API.");
            const errorText = await response.text();
            console.error("Détails de l'erreur :", errorText);
        }

        const films = await response.json();

        const resultsDiv = document.getElementById("resulta");
        resultsDiv.innerHTML = "";

        if (films.length === 0) {
            resultsDiv.innerHTML = "<p>Aucun film trouvé.</p>";
        } else {
            films.forEach((f) => {
                const movieDiv = document.createElement("div");
                movieDiv.classList.add("movie");
                movieDiv.innerHTML = `
                    <h3>${f.title}</h3>
                    <p><strong>____________________</strong></p>
                    <p><strong>Acteur principal :</strong> ${f.lead_actor}</p>
                    <p><strong>Genre :</strong> ${f.genre}</p>
                    <p><strong>Durée :</strong> ${f.duration_minutes} minutes</p>
                `;
                resultsDiv.appendChild(movieDiv);
            });
        }

    } catch (error) {
        console.error("Erreur réseau :", error);
        alert("Une erreur s'est produite lors de la recherche.");
    }

    document.getElementById("titre").value = "";
    document.getElementById("genre").value = "";
    document.getElementById("acteur_principal").value = "";
    document.getElementById("duree").value = "";
});
