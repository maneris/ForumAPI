import React, { useState} from 'react';
import {Modal,Button,Row,Col,Form, ModalHeader}  from 'react-bootstrap'
import {useParams,useNavigate} from 'react-router-dom';
function PostEdit(props){
    const params=useParams();
    const [show, setShow] = useState(false);
    const navigate = useNavigate();
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
  
    const handleSubmit= (event) =>{
        event.preventDefault()
        const form = event.currentTarget;
        fetch(process.env.REACT_APP_API+'topics/'+params.topicsId+"/threads/"+params.threadsId+"/posts/"+params.postsId,{
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json',
                'Authorization':"Bearer " + sessionStorage.getItem("token")
            },
            body:JSON.stringify({
                Description:form.Description.value,
            })
        })
        .then((response) => {
            if (!response.ok) {
                if(response.status==401){
                    sessionStorage.clear();
                    alert("Session expired please relogin.")
                    navigate('/login');
                }
                else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }else{
                handleClose();
                props.reload();
            }        
        })

    }
    return(
        <>
            <button type="button" className='btn btn-secondary' onClick={handleShow} >Edit</button>
            <Modal
            show={show}
            onHide={handleClose}
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered>
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-cventer">
                    Update post
                </Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Row>
                    <Col sm={12}>
                        <Form onSubmit={handleSubmit}>
                            <Form.Group controlId="Description">
                                <Form.Label>New description</Form.Label>
                                <Form.Control type="text" name="Description" required
                                defaultValue={props.Description} 
                                placeholder="ChapterId"/>
                            </Form.Group>
                            <Form.Group className='text-center' style={{marginTop:20}}>
                                <Button variant="primary" type="submit">Update Post</Button>
                            </Form.Group>
                        </Form>
                    </Col>
                </Row>
            </Modal.Body>
            </Modal>
        </>
    )
}
export default PostEdit;