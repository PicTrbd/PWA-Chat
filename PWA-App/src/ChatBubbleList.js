import React, { Component } from 'react';
import './styles/App.css';

class ChatBubbleList extends Component {

  getFormattedTimeFromDate(dateStr) {
      var date = new Date(dateStr);
      var timeStr = date.getHours() + ":" + (date.getMinutes()<10?'0':'') + date.getMinutes();
      return timeStr;
  }

  render() {
    return (
      <div className="container">
          {
            this.props.messages.map(function(message) {
              if (message.sender === this.props.userId) {
                return (
                  <div key={message.id}>
                    <li className="self">
                      <div className="msg tri-right talk-bubble round right-in">
                        <p>{message.message}</p>
                        <time>{this.getFormattedTimeFromDate(message.date)}</time>
                      </div>
                    </li>
                  </div>
                )
              }
              else {
                return (
                  <div key={message.id}>
                    <li className="other">
                      <div className="msg tri-right talk-bubble round left-in">
                        <p>{message.message}</p>
                        <div className="bubble-infos">
                          <time>{this.getFormattedTimeFromDate(message.date)}</time>
                          <p className="sender">{'#' + message.sender.substr(0, 8)}</p>
                        </div>
                      </div>
                    </li>
                  </div>
                )
              }
            }.bind(this))
          }
        </div>
      );
    }
}

export default ChatBubbleList;