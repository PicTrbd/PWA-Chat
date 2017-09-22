import React from 'react'

function getFormattedTimeFromDate(dateStr) {
    var date = new Date(dateStr);
    var timeStr = date.getHours() + ":" + (date.getMinutes()<10?'0':'') + date.getMinutes();
    return timeStr;
}

const MessageList = ({ messages, userId }) => (
    <ol className="chat">    
        <div className="container">
        {
            messages.map(message => {
                if (message.UserId === userId) {
                    return (
                        <div key={message.Id}>
                            <li className="self">
                                <div className="msg tri-right talk-bubble round right-in">
                                <p>{message.Message}</p>
                                <time>{getFormattedTimeFromDate(message.Date)}</time>
                                </div>
                            </li>
                        </div>
                    )
                }
                else {
                    return (
                        <div key={message.Id}>
                            <li className="other">
                                <div className="msg tri-right talk-bubble round left-in">
                                <p>{message.Message}</p>
                                <div className="bubble-infos">
                                    <time>{getFormattedTimeFromDate(message.Date)}</time>
                                    <p className="sender">{'#' + message.UserId.substr(0, 8)}</p>
                                </div>
                                </div>
                            </li>
                        </div>
                    )
                }
            })
        }
        </div>
    </ol>
)

export default MessageList