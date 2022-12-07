import '../functionalComponents/CustomStyles.css';
import React, {useState, useEffect} from 'react';
import {useNavigate,useParams} from 'react-router-dom';
import PostCreate from './Modals/PostCreate';
import ThreadEdit from './Modals/ThreadEdit';
import Spinner from 'react-bootstrap/Spinner';
import LogoImage from '../functionalComponents/ContentImg2.jpg'

function Threads () {
    const [thread,setThread] = useState(null);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();
    const params=useParams();
    useEffect(() => {
        fetch(process.env.REACT_APP_API+'topics/'+params.topicsId+"/threads/"+params.threadsId,{
            method:'get',
            mode: 'cors',
            headers:{
                'Authorization':"Bearer " + sessionStorage.getItem("token")
            },
        })
        .then((response) => {
            if (!response.ok) {
                if(response.status==401){
                    sessionStorage.clear();
                    alert("Session expired please relogin.")
                    navigate('/login');
                }else if(response.status==404){
                    alert("Requested thread does not exist.")
                    navigate('/topics/'+params.topicsId+"/threads");
                }
                else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }
            return response.json();           
        })
        .then(data=>{
            setThread(data);
            setLoading(false);
            console.log(data);
        });
    }, [loading])
    async function Delete(){
        await fetch(process.env.REACT_APP_API+'topics/'+params.topicsId+"/threads/"+params.threadsId,{
            method:'delete',
            mode: 'cors',
            headers:{
                'Authorization':"Bearer " + sessionStorage.getItem("token")
            },
        })
        .then((response) => {
            if (!response.ok) {
                if(response.status==401){
                    sessionStorage.clear();
                    alert("Session expired please relogin.")
                    navigate('/login');
                }else if(response.status==403){
                    alert("Insufficient privilages.")
                }
                else{
                    alert("Error:"+response.status+"\nMessage:"+response.statusText)
                }
            }else{
                navigate('/topics/'+params.topicsId+"/threads");
            }          
        })
    }
    function Reload(){
        setLoading(true);
    }

    return(
        <div className='container col-md-10 offset-md-1 mt-5 mb-5 p-3' style={{ backgroundImage: `url(${LogoImage})`, backgroundSize:`cover`, backgroundRepeat:'no-repeat', backgroundPosition:'center', height:'100%' }}>
        {loading ? (
            <div style={{color:'rgb(255,255,255)'}}>
            <Spinner animation="border" role="status">
                <span className="visually-hidden">Loading...</span></Spinner>
            A moment please... 
            </div>
            ) : (
            <div >
                <p className='title'>{thread.title}</p>
                <p className='description'>{thread.description}</p>
                <div className='container-fluid d-flex justify-content-end'>
                    <div className='btn-group' >
                        <ThreadEdit Title={thread.title} Description={thread.description} reload={()=>Reload()}/>
                        <button type="button" className='btn btn-secondary' onClick={()=>Delete()} >Delete</button>
                        <PostCreate />
                    </div>
                </div>  
            </div>
        )}
    </div>
    )
}
export default Threads;