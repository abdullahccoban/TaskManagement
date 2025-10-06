document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("loginForm");
    const loginButton = document.getElementById("loginButton");
    const loginError = document.getElementById("loginError");

    form.addEventListener("submit", async function (e) {
        e.preventDefault();

        loginButton.disabled = true;
        const originalText = loginButton.textContent;
        loginButton.textContent = "Waiting...";
        loginButton.classList.add("opacity-50", "cursor-not-allowed");

        loginError.classList.add("hidden");

        const email = document.getElementById("email").value;
        const password = document.getElementById("password").value;

        try {
            const response = await fetch("/account/login", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ email, password })
            });

            if (response.status === 200) {
                window.location.href = "/Home/Index";
            } else {
                loginError.classList.remove("hidden");
            }
        } catch (err) {
            loginError.classList.remove("hidden");
        } finally {
            loginButton.disabled = false;
            loginButton.textContent = originalText;
            loginButton.classList.remove("opacity-50", "cursor-not-allowed");
        }
    });
});