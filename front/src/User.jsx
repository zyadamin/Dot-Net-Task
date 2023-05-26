import React  from "react";
import { useLocation, useNavigate } from "../node_modules/react-router-dom/dist/index";

export const User = (props) => {
    
    const {state} = useLocation();
    const {userName,firstName,familyName,fatherName,address,birthdate} = state; // Read values passed on state
    const navigate = useNavigate();
    const handleSubmit = (e) => {
        e.preventDefault();
        navigate("/Reset",{ state: { userName: userName}});
        }

    return (
        <div className="auth-form-container">
            <h2>User Data</h2>
            <form className="login-form" onSubmit={handleSubmit}>
                <h1>{userName}</h1>
                <h1>{firstName}</h1>
                <h1>{fatherName}</h1>
                <h1>{familyName}</h1>
                <h1>{address}</h1>
                <h1>{birthdate}</h1>

                <button type="submit">Reset</button>
            </form>
        </div>
    )
}