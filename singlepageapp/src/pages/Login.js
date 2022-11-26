import React, {useState} from 'react';
import '../functionalComponents/CustomStyles.css';
import Register from './Register';
import {useNavigate} from 'react-router-dom'

export default function Login(){
    const navigate = useNavigate()
    const handleSubmit = (event) => {
        const form = event.currentTarget;
        fetch(process.env.REACT_APP_API+'login'+{
            method:'post',
            mode: 'cors',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                UserName:form.username.value,
                Password:form.password.value
            })
        })
        .then((response) => {
            if (!response.ok) {

                alert("Error:"+response.status)
            }
            return response.json();
          })
        .then(data=>{
            sessionStorage.setItem("token", data.accesstoken);
            console.log(data);
            navigate('/topics');
        });
    };
    async function handleAnonBrowse(){
        await fetch(process.env.REACT_APP_API+'AnonBrowse',{
                method: "post",
                mode: 'cors',
                })
            .then((response) => {
                if (!response.ok) {
                    console.log(response);
                }
                return response.json();
            })
            .then((actualData) => {
                console.log(actualData);
                sessionStorage.setItem("token", actualData.accessToken);
                navigate('/topics');
            });
            
    };
    function registerClick(){
        navigate('/register');
    }

    return (
        <div className="container ">
            <div className="row">
                <div className="col-md-4 offset-md-4 bg-light mt-4 p-4">  {/* vienoje vietoje persikelia i kita vieta  */}
                    <div className="login-form ">
                        <form className="row g-3" onSubmit={handleSubmit}>
                            <h4>Prisijungti</h4>
                            <div className="col-12">
                                <label>Username</label>
                                <input type="text" name="username" className="form-control" placeholder="Username" />
                            </div>
                            <div className="col-12">
                                <label>Password</label>
                                <input type="password" name="password" className="form-control" placeholder="Password"/>
                            </div>
                            <div className="col-sm-12">
                                <button type="submit" className="btn btn-primary col-sm-4">Login</button>
                                <button type="button" className="btn btn-info col-sm-7 offset-sm-1 "onClick={registerClick}>Register</button>
                            </div>
                        </form>
                        <div className="col-md-12 mt-2">
                            <button onClick={handleAnonBrowse} className="btn btn-secondary float-left">Continue as Anon</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>     
    )
  }
