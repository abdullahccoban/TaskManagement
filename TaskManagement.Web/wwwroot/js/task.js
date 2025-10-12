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

async function softDeleteTask(id) {
    try {
        const response = await fetch("/Task/SoftDelete", {
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

async function openStatusEdit(id) {
    const editStatus = document.getElementById(`div_edit_status_${id}`);
    const divStatus = document.getElementById(`div_status_${id}`);

    editStatus.classList.remove("hidden");
    divStatus.classList.add("hidden");
}

async function closeStatusEdit(id) {
    const editStatus = document.getElementById(`div_edit_status_${id}`);
    const divStatus = document.getElementById(`div_status_${id}`);

    editStatus.classList.add("hidden");
    divStatus.classList.remove("hidden");
}

async function saveStatusEdit(id) {
    let taskStatusName = document.getElementById(`task_status_name_${id}`);
    const selectStatus = document.getElementById(`status2_${id}`);
    const statusUpdateText = document.getElementById(`statusUpdateText_${id}`);
    statusUpdateText.textContent = "Kaydediliyor...";
    statusUpdateText.classList.remove("hidden");
    let json = JSON.stringify({ id: id, statusId: parseInt(selectStatus.value) });
    console.log(json);

    try {
        const response = await fetch("/Task/UpdateTaskStatus", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: json
        });

        if (response.status === 200) {
            statusUpdateText.textContent = "Kaydedildi.";
            taskStatusName.textContent = selectStatus.options[selectStatus.selectedIndex].text;
            setTimeout(() => {
                statusUpdateText.classList.add("hidden");
                statusUpdateText.textContent = "";
                closeStatusEdit(id);
            }                
            , 1000);
        }
    } catch (err) {
        location.reload();
    }
}

async function openUserEdit(id) {
    const editUser = document.getElementById(`div_edit_user_${id}`);
    const divUser = document.getElementById(`div_user_${id}`);

    editUser.classList.remove("hidden");
    divUser.classList.add("hidden");
}

async function closeUserEdit(id) {
    const editUser = document.getElementById(`div_edit_user_${id}`);
    const divUser = document.getElementById(`div_user_${id}`);

    editUser.classList.add("hidden");
    divUser.classList.remove("hidden");
}

async function saveUserEdit(id) {
    let taskUserName = document.getElementById(`task_user_name_${id}`);
    const selectUser = document.getElementById(`assignedUser2_${id}`);
    const userUpdateText = document.getElementById(`userUpdateText_${id}`);
    userUpdateText.textContent = "Kaydediliyor...";
    userUpdateText.classList.remove("hidden");
    let json = JSON.stringify({ id: id, userId: parseInt(selectUser.value) });

    try {
        const response = await fetch("/Task/UpdateTaskUser", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: json
        });

        if (response.status === 200) {
            userUpdateText.textContent = "Kaydedildi.";
            taskUserName.textContent = selectUser.options[selectUser.selectedIndex].text;
            setTimeout(() => {
                userUpdateText.classList.add("hidden");
                userUpdateText.textContent = "";
                closeUserEdit(id);
            }                
            , 1000);
        }
    } catch (err) {
        location.reload();
    }
}

let skip = 10;
const take = 10;

const loadMoreButtonDiv = document.getElementById("loadMoreButton");

async function loadMoreTasks(groupId) {
    let userRole = document.getElementById("user_role").value;
    try {
        const response = await fetch(`/Task/GetTasksLoadMore?groupId=${groupId}&skip=${skip}&take=${take}`);
        if (!response.ok) throw new Error("Load failed");

        const tasks = await response.json();

        const tableBody = document.getElementById("tBodyAllTasks");
        tasks.forEach(task => {
            const row = document.createElement("tr");
            row.classList.add("odd:bg-white");
            row.classList.add("even:bg-gray-50");
            row.innerHTML = `
                <td class="px-6 py-4 text-xs font-bold text-gray-600">${task.displayTaskCode}</td>
                <td class="px-6 py-4">${task.title}</td>
                <td class="px-6 py-4">${task.desc}</td>
                <td class="px-6 py-4">
                    <div id="div_status_${task.id}" class="flex justify-center items-center">
                        <p id="task_status_name_${task.id}" class="text-xs font-bold">${task.statusName}</p>
                        <a class="mx-1" onclick="openStatusEdit(${task.id})">
                            <svg class="w-5 h-5 text-gray-600" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m14.304 4.844 2.852 2.852M7 7H4a1 1 0 0 0-1 1v10a1 1 0 0 0 1 1h11a1 1 0 0 0 1-1v-4.5m2.409-9.91a2.017 2.017 0 0 1 0 2.853l-6.844 6.844L8 14l.713-3.565 6.844-6.844a2.015 2.015 0 0 1 2.852 0Z"/>
                            </svg>
                        </a>
                    </div>
                    <div id="div_edit_status_${task.id}" class="hidden flex items-center">
                        <select id="status2_${task.id}" class="bg-gray-50 border border-gray-300 text-gray-900 text-xs rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-1">
                            ${window.statuses.map(s => `<option value="${s.id}" ${s.id === task.statusId ? "selected" : ""}>${s.status}</option>`).join("")}                   
                        </select>
                        <a class="mx-1" onclick="saveStatusEdit(${task.id})">
                            <svg class="w-5 h-5 text-green-600" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.5 11.5 11 14l4-4m6 2a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"/>
                            </svg>
                        </a>
                        <p id="statusUpdateText_${task.id}" class="text-green-600 font-bold text-xs"></p>
                    </div>
                </td>
                <td class="px-6 py-4">
                    <div id="div_user_${task.id}" class="flex justify-center items-center">
                        <p id="task_user_name_${task.id}" class="text-xs font-bold">${task.username}</p>
                        ${window.userRole === "GroupAdmin" ? 
                        `<a class="mx-1" onclick="openUserEdit(${task.id})">
                            <svg class="w-5 h-5 text-gray-600" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="m14.304 4.844 2.852 2.852M7 7H4a1 1 0 0 0-1 1v10a1 1 0 0 0 1 1h11a1 1 0 0 0 1-1v-4.5m2.409-9.91a2.017 2.017 0 0 1 0 2.853l-6.844 6.844L8 14l.713-3.565 6.844-6.844a2.015 2.015 0 0 1 2.852 0Z"/>
                            </svg>
                        </a>` 
                        : ""}
                    </div>
                    <div id="div_edit_user_${task.id}" class="hidden flex items-center">
                        <select id="assignedUser2_${task.id}" class="bg-gray-50 border border-gray-300 text-gray-900 text-xs rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-1">
                            ${window.groupMembers.map(s => `<option value="${s.userId}" ${s.userId === task.userId ? "selected" : ""}>${s.username}</option>`).join("")}
                        </select>
                        <a class="mx-1" onclick="saveUserEdit(${task.id})">
                            <svg class="w-5 h-5 text-green-600" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" width="24" height="24" fill="none" viewBox="0 0 24 24">
                                <path stroke="currentColor" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8.5 11.5 11 14l4-4m6 2a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z"/>
                            </svg>
                        </a>
                        <p id="userUpdateText_${task.id}" class="text-green-600 font-bold text-xs"></p>
                    </div>
                </td>   
                <td class="px-6 py-4">${userRole === "GroupAdmin" ? `<button type="button" class="text-white bg-gray-700 hover:bg-red-800 rounded-full text-xs px-1.5 py-0.5 text-center" onclick="softDeleteTask(${task.id})">delete</button>` : ""}</td>`;
            tableBody.appendChild(row);            
        });

        skip += tasks.length;

        if (tasks.length < take) {
            loadMoreButtonDiv.classList.add("hidden");
        }
    
    } catch(err) {
        location.reload();
    }
}   