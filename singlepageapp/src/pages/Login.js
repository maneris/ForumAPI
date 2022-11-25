import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import React, {useState} from 'react';
import { Modal } from 'bootstrap';

export default function Login(){
    const [token, setToken] = useState(null);
    const [postResult, setPostResult] = useState(null);
    const handleSubmit = (event) => {
        // const form = event.currentTarget;
        // fetch(process.env.REACT_APP_API+'login')
        // .then((response) => {
        //     if (!response.ok) {
        //       Modal()
        //     }
        //     return response.json();
        //   })
        // .then(data=>{
        //     setToken({accessToken: data.accessToken});
        // });
    };
    async function handleAnonBrowse(){
        console.log("K1");
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
            setToken({
                accesstoken: actualData.accesstoken
            })
            console.log(actualData);
        });
            
    };

    return (
        <>
        <Form >
            <Form.Group className="mb-3" controlId="formUsername">
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Enter username" />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Enter password" />
            </Form.Group>
            <Button variant="primary" type="submit" >
                Login
            </Button>
        </Form>
        <Button variant="secondary" onClick={handleAnonBrowse}>Anonymous </Button>
        </>
    )
  }
