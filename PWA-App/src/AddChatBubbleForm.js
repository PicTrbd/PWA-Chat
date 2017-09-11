import React, { Component } from 'react';
import './styles/App.css';
import * as guid from 'guid'

class AddChatBubbleForm extends Component {
    
    createMessage(e) {
        e.preventDefault();
        var newMessage = 
        {
            Message: this.refs.bubbleText.value,
            UserId: this.props.userId,
            Id: guid.raw(),
            Date: new Date()
        };
        if(typeof newMessage.Message === 'string' && newMessage.Message.length > 0) {
            this.props.addMessage(newMessage);
            this.refs.chatBubbleForm.reset();
        }
    }

    render() {
        return(
        <form className="form-inline" ref="chatBubbleForm" onSubmit={this.createMessage.bind(this)}>
            <input id="submit-button" type="button" value="Envoyer" onClick={this.createMessage.bind(this)} />
            <div id="textarea-container">
                <input className="textarea" type="text" ref="bubbleText" placeholder="Tapez ici votre message"/>
            </div>
        </form>
        )
    }
}

export default AddChatBubbleForm;