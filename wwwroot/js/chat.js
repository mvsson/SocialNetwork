class Message {
    constructor(username, text, when) {
        this.name = username;
        this.text = text;
        this.dateadded = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    let when = document.createElement('span');
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;
    
    let when = new Date();
    let message = new Message(username, text, when);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.name == username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "container darker bg-primary" : "container bg-light";

    let sender = document.createElement('p');
    sender.className = "sender";
    sender.className += isCurrentUserMessage ? " text-right text-white" : " text-left"; /////////
    sender.innerHTML = message.name;

    let text = document.createElement('p');
    text.className = isCurrentUserMessage ? "text-right text-white" : "text-left"; ////////
    text.innerHTML = message.text;

    let when = document.createElement('span');
    when.className = isCurrentUserMessage ? "time-right text-light" : "text-right";

    var currentdate = new Date();
    when.innerHTML = 
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    let row = document.createElement('div');
    row.className = "row";
    let div = document.createElement('div');
    div.className = isCurrentUserMessage ? "col-md-6 offset-md-6" : "";

    container.appendChild(sender);
    container.appendChild(text);
    container.appendChild(when);
    div.appendChild(container);
    row.appendChild(div);
    chat.appendChild(row);
}