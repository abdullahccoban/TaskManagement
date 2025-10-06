const createStatusForm = document.getElementById("createStatusForm");
const createStatusButton = document.getElementById("createStatusButton");

createStatusForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    createStatusButton.disabled = true;
    const originalText = createStatusButton.textContent;
    createStatusButton.textContent = "Waiting...";
    createStatusButton.classList.add("opacity-50", "cursor-not-allowed");

    const status = document.getElementById("statusName").value;
    const groupId = document.getElementById("groupStatusId").value;

    try {
        const response = await fetch("/api/Status/CreateStatus", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ status, groupId })
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

async function openEditInput(id) {
    const input = document.getElementById(`edit_input_${id}`);
    const text = document.getElementById(`edit_span_${id}`);
    const buttonGroup = document.getElementById(`button_group_${id}`);
    const editGroup = document.getElementById(`edit_group_${id}`);
    input.classList.remove("hidden");
    editGroup.classList.remove("hidden");
    text.classList.add("hidden");
    buttonGroup.classList.add("hidden");
    buttonGroup.classList.remove("inline-flex");
}

async function closeEditInput(id) {
    const input = document.getElementById(`edit_input_${id}`);
    const text = document.getElementById(`edit_span_${id}`);
    const buttonGroup = document.getElementById(`button_group_${id}`);
    const editGroup = document.getElementById(`edit_group_${id}`);
    input.classList.add("hidden");
    editGroup.classList.add("hidden");
    text.classList.remove("hidden");
    buttonGroup.classList.remove("hidden");
    buttonGroup.classList.add("inline-flex");
}

async function updateStatus(id) {
    const status = document.getElementById(`edit_input_${id}`).value;
    const groupId = document.getElementById("groupStatusId").value;

    try {
        const response = await fetch("/api/Status/UpdateStatus", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ id, status, groupId })
        });

        if (response.status === 200) {
            location.reload();
        }
    } catch (err) {
        location.reload();
    } finally {
        location.reload();
    }
}

async function deleteStatus(id) {
    try {
        const response = await fetch("/api/Status/DeleteStatus", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(id)
        });

        if (response.status === 200) {
            location.reload();
        }
    } catch (err) {
        location.reload();
    } finally {
        location.reload();
    }
}