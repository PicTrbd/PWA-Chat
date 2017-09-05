import React, { Component } from 'react';
import './styles/App.css';

class AddChatBubbleForm extends Component {
    
    createMessage(e) {
        e.preventDefault();
        var message = this.refs.bubbleText.value;
        if(typeof message === 'string' && message.length > 0) {
        this.props.addMessage(message);
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