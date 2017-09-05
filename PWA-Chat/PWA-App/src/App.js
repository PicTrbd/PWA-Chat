import React, { Component } from 'react';
import './styles/App.css';

class App extends Component {
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
          <input id="submit-button" type="button" value="Envoyer" />
          <div id="textarea-container">
              <input className="textarea" type="text" placeholder="Tapez ici votre message"/>
          </div>
        </div>
      </div>
    </div>
    );
  }
}

export default App;
