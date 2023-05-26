import React, { useState } from "react";
import axios from "../node_modules/axios/index";
import { useNavigate } from "../node_modules/react-router-dom/dist/index";

export const Login = (props) => {

    const [email, setEmail] = useState('');
    const [pass, setPass] = useState('');
    const [res, setRes] = useState('');
    const navigate = useNavigate();
    const handleSubmit = (e) => {
        e.preventDefault();

           
            axios.post('http://localhost:5000/api/Persons/retrieve', {
                "userName": email,
                "password": pass,
              })
              .then(function (response) {
                
                setRes(response.data.message);
                console.log(response);
                console.log(response.data)
                if(response.data.success){

                    navigate("/User", { state: { userName: response.data.data.userName,
                        firstName: response.data.data.firstName,
                        fatherName:response.data.data.fatherName,
                        familyName:response.data.data.familyName,
                        address:response.data.data.address,
                        birthdate:response.data.data.birthdate } });

                }
                
            })
              .catch(function (error) {
                console.log(error);
              });

        }

        function handleClick() {
        navigate("/Register");
        }
        

    return (
        <div className="auth-form-container">
            <h2>Login</h2>
            <form className="login-form" onSubmit={handleSubmit}>
                <div className="response">{res}</div>
                <label htmlFor="email">email</label>
                <input value={email} onChange={(e) => setEmail(e.target.value)}type="text" placeholder="youremail@gmail.com" id="email" name="email" />
                <label htmlFor="password">password</label>
                <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="********" id="password" name="password" />
                <button type="submit">Log In</button>
            </form>
            <button className="link-btn" onClick={handleClick}>Don't have an account? Register here.</button>
        </div>
    )
}