import React, { Component } from 'react';
import './styles/App.css';

class ChatBubbleList extends Component {

  getActualTime() {
      var date = new Date();
      var datestr = date.getHours() + ":" + (date.getMinutes()<10?'0':'') + date.getMinutes();
      return datestr;
  }

  render() {
    return (
      <div className="container">
          {
            Object.keys(this.props.messages).map(function(key) { 
              return (
              <div key={this.props.messages[key]}>
                <li className="self">
                  <div className="msg tri-right talk-bubble round left-in">
                    <p>{this.props.messages[key]}</p>
                    <time>{this.getActualTime()}</time>
                  </div>
                </li>
                <li className="other">
                  <div className="msg tri-right talk-bubble round left-in">
                    <p>{this.props.messages[key]}</p>
                    <time>{this.getActualTime()}</time>
                  </div>
                </li>
              </div>
              )
            }.bind(this))
          }
        </div>
      );
    }
}

export default ChatBubbleList;