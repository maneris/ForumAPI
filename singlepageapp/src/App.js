import './App.css';
import Login from "./pages/Login";
import Register from "./pages/Register";
import Topic from "./pages/Topics";
import Thread from "./pages/Threads";
import Post from "./pages/Threads";
import { Routes, Route  } from 'react-router-dom';
import { Link, Outlet } from 'react-router-dom';
import Navbar from 'react-bootstrap/Navbar';


function App() {
  return (
    <>
        <Routes>
            <Route path='/topics/:topicsId' element={<Topic/>} >
              <Route path='threads/:threadsId' element={<Thread/>} >
                <Route path='posts/:postsId' element={<Post/>}/>
              </Route>
            </Route>
            <Route path='/login' element={<Login/>} />
            <Route path='/register' element={<Register/>} />
        </Routes>
    </>
  );
}
export default App;