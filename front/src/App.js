import React from "react";
import './App.css';

//import {BrowserRouter as Router,Switch,Route,}  from "react-router-dom";
import { Login } from './Login';
import { Register } from './Register';
import { Reset } from "./SetPassword";
import { BrowserRouter as Router,Route,Routes } from "../node_modules/react-router-dom/dist/index";
import { User } from "./User";

function App() {
  return (
    <div className="App">
      {
        <Router>
          <Routes>
            <Route path='/' element={<Login/>} />
            <Route path='/Register' element={<Register/>} />
            <Route path='/Reset' element={<Reset/>} />
            <Route path='/User' element={<User />} />

          </Routes>

        </Router>
  }
    </div>
  );
}

export default App;
