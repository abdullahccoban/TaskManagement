const createGroupForm = document.getElementById("createGroupForm");
const createGroupButton = document.getElementById("createGroupButton");

createGroupForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    createGroupButton.disabled = true;
    const originalText = createGroupButton.textContent;
    createGroupButton.textContent = "Waiting...";
    createGroupButton.classList.add("opacity-50", "cursor-not-allowed");

    const groupName = document.getElementById("groupName").value;
    debugger;
    try {
        const response = await fetch("/group/create", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ groupName })
        });

        if (response.status === 200) {
            location.reload();
        }
    } catch (err) {
        location.reload();
    } finally {
        location.reload();
    }
});