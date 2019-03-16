import React, { Component } from 'react';
import ExchangeRates from './ExchangeRates/ExchangeRates.js'
import logo from './logo.svg';
import './App.css';

class App extends Component {
  render() {
    return (
      <div className="App">
        <header className="App-header">
        <div className ="Logo-container">
          <img src={logo} className="App-logo" alt="logo" />
        </div>
        </header>
        <ExchangeRates apiHost="http://localhost:53383" />
      </div>
    );
  }
}

export default App;
