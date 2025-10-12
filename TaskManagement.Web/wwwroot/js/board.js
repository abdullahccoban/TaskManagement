let draggedTaskId = null;

async function onDragStart(event) {
    draggedTaskId = event.target.dataset.taskId;
}

async function onDrop(event) {
    const newStatusId = event.currentTarget.dataset.statusId;
    
    try {
        const response = await fetch("/Task/UpdateTaskStatus", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ id: draggedTaskId, statusId: newStatusId })
        });

        if (response.status === 200) {
            location.reload();
        }
    } catch (err) {
        location.reload();
    }
}