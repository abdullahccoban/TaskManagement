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

async function updateGroup(id) {
    const groupName = document.getElementById(`edit_input_${id}`).value;
    try {
        const response = await fetch("/Group/Update", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ id, groupName })
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

async function deleteGroup(id) {
    try {
        const response = await fetch("/Group/Delete", {
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