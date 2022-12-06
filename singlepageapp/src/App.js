import './App.css';
import Login from "./pages/Login";
import Register from "./pages/Register";
import Posts from "./pages/Posts";
import TopicsList from "./pages/TopicsList";
import ThreadsList from "./pages/ThreadsList";
import PostsList from "./pages/PostsList";
import { Routes, Route  } from 'react-router-dom';
import DynNavBar from './functionalComponents/NavBar';
import FooterContent from './functionalComponents/FooterContent'


function App() {
  return (
    <>
      <header className='App-header'>
        <DynNavBar/>
      </header>
      <main className='App-Main'>
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
      <footer className='App-Footer'>
        <FooterContent/>
      </footer>
    </>
  );
}
export default App;

