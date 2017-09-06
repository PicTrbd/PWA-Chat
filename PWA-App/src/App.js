import React, { Component } from 'react';
import './styles/App.css';

class App extends Component {

  componentDidMount() {
    this.retrieveMessages()
  }

  retrieveMessages = function () {
    var self = this;
    setInterval(function(){
      var response = self.get("messages", {}).then((value) => {
        console.log(value)
      })
    }, 10000)
  }

  handleFetch = function(path, input) {
    input.headers = {'Content-Type': 'application/json'}
    var request = fetch("http://localhost:8080/api/" + path, input)
      .then(response => {
        console.log(response);
        if (response.status === 200)
          return response.json();        
        })
    return request;
  }

  get = function(path, body) {
    return this.handleFetch(path, { method: 'get', mode: 'cors'});
  };

  post = function(path, body) {
    return this.handleFetch(path, { method: 'post', mode: 'no-cors', body: JSON.stringify(body) });
  };

  sendMessage = function() {
    var params = 
    {
      sender: "Paul President",
      message: this.inputTitle.value
    };
    this.post("message", params);
    this.inputTitle.value = ""
  };

  render() {
    return (
      <div className="mdl-layout mdl-js-layout mdl-layout--fixed-header">
      <header>
        <div className="mdl-layout--large-screen-only top-bar">
          <h3>Progressive Web App - Chat</h3>
        </div>
        <div className="tab-bar mdl-layout__tab-bar">
          <a className="mdl-layout__tab isActive">PWA - Chat</a>
        </div>
      </header>

      <div id="chatbox">
        <ol className="chat">
          <li className="other">
            <div className="msg tri-right talk-bubble round left-in">
              <p>Salut salut, t'as deux minutes ?</p>
              <time>20:17</time>
            </div>
          </li>
          <li className="self">
            <div className="msg tri-right talk-bubble round right-in">
              <p>Oui bien sur.</p>
              <time>20:18</time>
            </div>
          </li>
        </ol>
        <div id="bottom-area">
          <input id="submit-button" type="button" value="Envoyer" onClick={this.sendMessage.bind(this)}/>
          <div id="textarea-container">
              <input className="textarea" ref={el => this.inputTitle = el} type="text" placeholder="Tapez ici votre message"/>
          </div>
        </div>
      </div>
    </div>
    );
  }
}

export default App;
