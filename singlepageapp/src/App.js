import './App.css';
import Login from "./pages/Login";
import Register from "./pages/Register";
import Posts from "./pages/Posts";
import TopicsList from "./pages/TopicsList";
import ThreadsList from "./pages/ThreadsList";
import PostsList from "./pages/PostsList";
import { Routes, Route  } from 'react-router-dom';


function App() {
  return (
    <main >
      <Routes>
        <Route path='/topics' element={<TopicsList/>} />
        <Route path='/topics/:topicsId/threads' element={<ThreadsList/>} />
        <Route path='/topics/:topicsId/threads/:threadsId/posts' element={<PostsList/>}/>
        <Route path='/topics/:topicsId/threads/:threadsId/posts/:postsId' element={<Posts/>}/>
        <Route path='/login' element={<Login/>} />
        <Route path='/register' element={<Register/>} />
        <Route path='*' element={<Login/> }/>
      </Routes>
    </main>
  );
}
export default App;

