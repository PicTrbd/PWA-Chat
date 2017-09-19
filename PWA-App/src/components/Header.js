import React, { Component } from 'react';

class Header extends Component
{
    render() {
        return (
            <header>
                <div className="mdl-layout--large-screen-only top-bar">
                <h3>Progressive Web App - Chat</h3>
                </div>
                <div className="tab-bar mdl-layout__tab-bar">
                <a className="mdl-layout__tab isActive">PWA - Chat</a>
                </div>
            </header>
        );
    }
}

export default Header;