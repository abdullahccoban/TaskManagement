const createTaskForm = document.getElementById("createTaskForm");
const createTaskButton = document.getElementById("createTaskButton");

createTaskForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    createTaskButton.disabled = true;
    createTaskButton.textContent = "Waiting...";
    createTaskButton.classList.add("opacity-50", "cursor-not-allowed");

    let title = document.getElementById("title").value;
    let desc = document.getElementById("desc").value;;
    let groupId = document.getElementById("groupId").value;;
    let userId = document.getElementById("assignedUser").value;;
    let statusId = document.getElementById("status").value;;
    
    let json = JSON.stringify({ title, desc, groupId, userId, statusId });
    console.log(json);

    try {
        const response = await fetch("/task/create", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: json
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