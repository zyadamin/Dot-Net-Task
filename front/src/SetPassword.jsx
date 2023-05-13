import React, { useState } from "react";
import axios from "../node_modules/axios/index";
import { useLocation, useNavigate } from "../node_modules/react-router-dom/dist/index";

export const Reset = (props) => {
    
    const [oldPassword, setOldPassword] = useState('');
    const [newPassword, setNewPassword] = useState('');
    const {state} = useLocation();
    const {userName} = state; // Read values passed on state

    const navigate = useNavigate();
    
    const handleSubmit = (e) => {
        e.preventDefault();
           

        const article = {
            "userName":userName ,
            "password": oldPassword,
            "newPassword":newPassword
    };
        axios.put('http://localhost:5000/api/Persons/resetpassword', article);
        navigate("/");
        }

    return (
        <div className="auth-form-container">
            <h2>Reset Password</h2>
            <form className="Reset-form" onSubmit={handleSubmit}>
                <label htmlFor="oldPassword">OldPassword</label>
                <input value={oldPassword} onChange={(e) => setOldPassword(e.target.value)} type="password" placeholder="*********" id="oldPassword" name="oldPassword" />
                
                <label htmlFor="newPassword">newPassword</label>
                <input value={newPassword} onChange={(e) => setNewPassword(e.target.value)} type="password" placeholder="********" id="newPassword" name="newPassword" />
                <button type="submit">Reset</button>
            </form>
        </div>
    )
}