import { NavLink } from 'react-bootstrap';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

function DynNavBar() {
    const LogOut = () =>{
        sessionStorage.clear();
    }


  return (
    <Navbar style={{backGroundColor:'rgb(91, 75, 138)'}} expand="sm" className='offset-md-2'>
      <Container>
        <Navbar.Brand href="/"><img
            alt=""
            src="/CustomLogo.svg"
            width="30"
            height="30"
            className="d-inline-block align-top"
            /> Manto Forum</Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav" className='offset-md-2' >
          <Nav className="me-auto">
            <Nav.Link className='label' href="/topics"><p style={{color:'rgb(255, 255, 255)', fontWeight:"600", fontFamily:'cursive'}}>Topics</p></Nav.Link>
            <Nav.Link href="/login" onClick={LogOut}><p style={{color:'rgb(255, 255, 255)', fontWeight:"600",fontFamily:'cursive'}}>Log Out</p></Nav.Link>
            <Nav.Link href="https://github.com/maneris/ForumAPI"><p style={{color:'rgb(255, 255, 255)', fontWeight:"600",fontFamily:'cursive'}}>Git Page</p></Nav.Link>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default DynNavBar;