import React from 'react'

function resetInputAndSendMessage(e, userId, currentChannel, onSendMessage) {
    e.preventDefault();
    var inputMsg = document.getElementById("inputMsg");
    
    if(typeof inputMsg.value === 'string' && inputMsg.value.length > 0) {
        onSendMessage(userId, inputMsg.value, currentChannel);
        inputMsg.value = "";        
    }
}

const AddMessage = ({ userId, currentChannel, onSendMessage }) => (
    <div id="bottom-area">    
        <form className="form-inline" onSubmit={(e) => {resetInputAndSendMessage(e, userId, currentChannel, onSendMessage)}}>
            <input id="submit-button" type="button" value="Send" onClick={(e) => 
                {resetInputAndSendMessage(e, userId, currentChannel, onSendMessage)}} />
            <div id="textarea-container">
                <input id="inputMsg" className="textarea" type="text" placeholder="Type your new message"/>
            </div>
        </form>
    </div>
)

export default AddMessage;