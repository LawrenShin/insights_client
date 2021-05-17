import React from 'react';
import {makeStyles} from "@material-ui/core";
import DiSvg from "./DiSvg";
import Profile from "./profile";
import {useHistory} from "react-router-dom";


const useStyles = makeStyles({
  header: {
    display: 'flex',
    background: '#fff',
    padding: '10px 30px',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
    minHeight: '6vh',
  },
  DiSvgContainer: {
    width: '100%',
    display: 'flex',
    alignItems: 'center',
    '& img': { width: '150px' },
    '& :hover': {
      cursor: 'pointer',
    }
  },
})

const Header = () => {
  const styles = useStyles();
  const history = useHistory();

  return (
    <div className={styles.header}>
      <DiSvg onClick={() => history.push('/mainSearch')} styles={styles.DiSvgContainer} />
      <Profile />
    </div>
  )
}

export default Header;
