import React, {useState} from 'react';
import ReactDOM from 'react-dom/client';
import {useNavigate} from 'react-router-dom';
function Register() {
    const navigate = useNavigate();
    const handleSubmit = (event) => {
        event.preventDefault()
        console.log("hi");
        const form = event.currentTarget;
        console.log(form);
        fetch(process.env.REACT_APP_API+'register',{
            method:'POST',
            mode: 'cors',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                UserName:form.username.value,
                Email:form.email.value,
                Password:form.password.value
            })
        })
        .then((response) => {
            if (!response.ok) {
                console.log(response);
                alert("Error:"+response.status+"\n"+response.statusText)
            }
            else{
                alert("successfuly registered");
                navigate('/login');
            }
            
        });
        
    };


    return (
        <div className="container ">
            <div className="row">
                <div className="col-md-4 offset-md-4 bg-light mt-4 p-4">  {/* vienoje vietoje persikelia i kita vieta  */}
                    <div className="register-form ">
                        <form className="row g-3" onSubmit={handleSubmit}>
                            <h4>Registruotis</h4>
                            <div className="col-12">
                                <label>Elektroninis paštas</label>
                                <input type="email" name="email" className="form-control" placeholder="email@email.com"/>
                            </div>
                            <div className="col-12">
                                <label>Username</label>
                                <input type="text" name="username" className="form-control" placeholder="Username" />
                            </div>
                            <div className="col-12">
                                <label>Password</label>
                                <input type="password" name="password" className="form-control" placeholder="Password"/>
                            </div>
                            <div className="col-sm-12">
                                <button type="submit" className="btn btn-primary col-sm-4">Register</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>     
    )
  }
  export default Register;