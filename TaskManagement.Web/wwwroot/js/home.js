async function createRequest(groupId) {
    let message = document.getElementById("requestMessage").value;
    try {
        const response = await fetch("/Group/CreateRequest", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ groupId, message })
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