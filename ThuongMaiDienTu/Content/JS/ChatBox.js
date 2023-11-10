
$(document).ready(function () {
    // Load danh sách người đã từng chat khi trang được tải
    loadUserList();
});
let ReceiverUserID;
// Function to load danh sách người đã từng chat
function loadUserList() {
    const userList = document.getElementById("user-list");
    $.ajax({
        type: "GET",
        url: "/ChatBox/GetUserList",
        dataType: "json",
        success: function (data) {
            data.forEach(function (user) {
                const userItem = document.createElement("li");
                userItem.className = "user";
                userItem.setAttribute("data-userid", user.userid);

                // Thêm mã xử lý sự kiện onclick để cập nhật ReceiverUserID
                userItem.onclick = function () {
                    ReceiverUserID = user.userid; // Cập nhật ReceiverUserID
                    loadChatHistory(ReceiverUserID);
                    const chatUsername = document.getElementById("chat-username");
                    chatUsername.textContent = user.HoVaTen;
                };

                userItem.textContent = user.HoVaTen;
                userList.appendChild(userItem);
            });
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}


// Function to load lịch sử trò chuyện với một người
function loadChatHistory(userID) {
    const chatHistory = document.getElementById("chat-history");
    chatHistory.innerHTML = ''; // Xóa lịch sử trò chuyện hiện tại

    $.ajax({
        type: "GET",
        url: "/ChatBox/GetChatHistory",
        data: { userID: userID },
        dataType: "json",
        success: function (data) {
            data.forEach(function (message) {
                const isUser = message.UserID === userID;
                const messageDiv = document.createElement("div");
                messageDiv.className = "message" + (isUser ? " other-message" : " user-message");

                const contentDiv = document.createElement("div");
                contentDiv.className = "message-content";
                contentDiv.textContent = message.Message;
                messageDiv.appendChild(contentDiv);
                chatHistory.appendChild(messageDiv);

            });
        },
        error: function (error) {
            console.log("Error:", error);
        }
    });
}

//
//
//
//


function addMessageToChatHistory(message, isUser) {
    const chatHistory = document.getElementById("chat-history");

    const messageDiv = document.createElement("div");
    messageDiv.className = "message" + (isUser ? " user-message" : " other-message");

    const contentDiv = document.createElement("div");
    contentDiv.className = "message-content";
    contentDiv.textContent = message;
    messageDiv.appendChild(contentDiv);

    chatHistory.appendChild(messageDiv);

    // Cuộc trò chuyện tự động cuộn xuống để hiển thị tin nhắn mới nhất
    chatHistory.scrollTop = chatHistory.scrollHeight;
}


function sendMessage() {
    const input = document.getElementById("message-input");
    const message = input.value;
    const userID = ReceiverUserID; // Lấy giá trị userID từ giao diện người dùng

    if (message.trim() === "") {
        return; // Không gửi tin nhắn trống
    }

    $.ajax({
        type: "POST",
        url: "/ChatBox/SendMessage",
        data: { userID: userID, message: message },
        dataType: "json",
        success: function (response) {
            if (response.success) {
                // Tin nhắn gửi thành công, cập nhật lịch sử trò chuyện
                addMessageToChatHistory(message, true);
            } else {
                console.log("Lỗi khi gửi không cập nhật được:", response.error);
                console.log(response);
            }
        },
        error: function (error) {
            console.log("Lỗi khi gửi tin nhắn:", error);
        }
    });

    // Xóa nội dung khỏi input sau khi gửi
    input.value = "";
}

