const registerForm = document.getElementById("registerForm");
const registerButton = document.getElementById("registerButton");
const registerError = document.getElementById("registerError");
const registerErrorMessage = document.getElementById("registerErrorMessage");

registerForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    registerButton.disabled = true;
    const originalText = registerButton.textContent;
    registerButton.textContent = "Waiting...";
    registerButton.classList.add("opacity-50", "cursor-not-allowed");

    registerError.classList.add("hidden");

    const name = document.getElementById("username").value; 
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const confirm_password = document.getElementById("confirm_password").value;

    if (password != confirm_password) {
        registerError.classList.remove("hidden");
        registerErrorMessage.textContent = "Password does not match!";
        registerButton.disabled = false;
        registerButton.textContent = originalText;
        registerButton.classList.remove("opacity-50", "cursor-not-allowed");
        return;
    }

    try {
        const response = await fetch("/account/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ name, email, password })
        });

        const data = await response.json();

        if (response.status === 200) {
            window.location.href = "/Account/Login";
        } else {
            registerErrorMessage.textContent = data.message;
            registerError.classList.remove("hidden");
        }
    } catch (err) {
        registerErrorMessage.textContent = "Register Error";
        registerError.classList.remove("hidden");
    } finally {
        registerButton.disabled = false;
        registerButton.textContent = originalText;
        registerButton.classList.remove("opacity-50", "cursor-not-allowed");
    }
});