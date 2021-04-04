class Message {
    constructor(username, text, when, dialogid) {
        this.name = username;
        this.text = text;
        this.dateadded = when;
        this.dialogId = dialogid;
    }
}

// userName and dialogId are declared in razor page.
const dialogId = currentDialogId;
const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];
chat.scrollTop = chat.scrollHeight;

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    let when = document.createElement('span');
    when.innerHTML =
        currentdate.getDate() +
        "." +
        (currentdate.getMonth() + 1) +
        "." +
        currentdate.getFullYear() +
        " " +
        currentdate.toLocaleString('ru', { hour: 'numeric', minute: 'numeric', second: 'numeric', hour12: false });
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let when = new Date();
    let dialogid = dialogId;
    let message = new Message(username, text, when, dialogid);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    if (dialogId !== message.dialogId) {
        return;
    }
    let isCurrentUserMessage = message.name == username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker bg-info" : "container bg-light";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.className += isCurrentUserMessage ? " text-right text-white" : " text-left"; 
    sender.innerHTML = message.name;

    let text = document.createElement('p');
    text.className = isCurrentUserMessage ? "text-right text-white" : "text-left"; 
    text.innerHTML = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-right text-light" : "time-right";

    var currentdate = new Date();
    when.innerHTML =
        currentdate.getDate() +
        "." +
        (currentdate.getMonth() + 1) +
        "." +
        currentdate.getFullYear() +
        " " +
        currentdate.toLocaleString('ru', { hour: 'numeric', minute: 'numeric', second: 'numeric',  hour12: false });

    let row = document.createElement('div');
    row.className = "row";
    let div = document.createElement('div');
    div.className = isCurrentUserMessage ? "col-md-6 offset-md-6" : "col-md-6";

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    div.appendChild(container);
    row.appendChild(div);
    chat.appendChild(row);
    chat.scrollTop=chat.scrollHeight;
}