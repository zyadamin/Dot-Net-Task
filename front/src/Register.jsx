import React, { useState } from "react";
import axios from "../node_modules/axios/index";
import { useNavigate } from "../node_modules/react-router-dom/dist/index";

export const Register = (props) => {
    const [userName, setUserName] = useState('');
    const [pass, setPass] = useState('');
    const [firstName, setFirtsName] = useState('');
    const [fatherName, setFatherName] = useState('');
    const [familyName, setFamilyName] = useState('');
    const [address, setAddress] = useState('');
    const [birthdate, setBirthdate] = useState('');
    const [res, setRes] = useState('');

    

    const navigate = useNavigate();

    const handleSubmit = (e) => {
        e.preventDefault();

        axios.post('http://localhost:5000/api/Persons/register', {
            "userName": userName,
            "password": pass,
            "firstName": firstName,
            "fatherName": fatherName,
            "familyName": familyName,
            "address": address,
            "birthdate": birthdate,
          })
          .then(function (response) {
            console.log(response);
            console.log(response.data)

            setRes(response.data.message);
            if(response.data.success){
                navigate("/");
            }
            
            
        }).catch(function (error) {
            console.log(error);
          });





    }

    return (
        <div className="auth-form-container">
            <h2>Register</h2>
            <div className="response">{res}</div>
        <form className="register-form" onSubmit={handleSubmit}>
            <label htmlFor="firstName">First Name</label>
            <input value={firstName} name="firstName" onChange={(e) => setFirtsName(e.target.value)} id="firstName" placeholder="First Name" />
            
            <label htmlFor="fatherName">Father Name</label>
            <input value={fatherName} name="fatherName" onChange={(e) => setFatherName(e.target.value)} id="fatherName" placeholder="Father Name" />
            
            <label htmlFor="familyName">Family Name</label>
            <input value={familyName} name="familyName" onChange={(e) => setFamilyName(e.target.value)} id="familyName" placeholder="Family Name" />
            
            <label htmlFor="address">Address</label>
            <input value={address} name="address" onChange={(e) => setAddress(e.target.value)} id="address" placeholder="Address" />

            <label htmlFor="birthdate">birthdate</label>
            <input value={birthdate} name="birthdate" onChange={(e) => setBirthdate(e.target.value)} id="birthdate" placeholder="birthdate" />

            <label htmlFor="userName">user Name</label>
            <input value={userName} onChange={(e) => setUserName(e.target.value)}type="text" placeholder="user name" id="userName" name="userName" />
            
            <label htmlFor="password">password</label>
            <input value={pass} onChange={(e) => setPass(e.target.value)} type="password" placeholder="********" id="password" name="password" />
            <button type="submit">Register</button>
        </form>
        <button className="link-btn" onClick={() => props.onFormSwitch('login')}>Already have an account? Login here.</button>
    </div>
    )
}
