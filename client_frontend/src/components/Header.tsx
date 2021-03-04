import React from 'react';
import {makeStyles} from "@material-ui/core";
import DiSvg from "./DiSvg";


const useStyles = makeStyles({
  header: {
    background: '#fff',
    padding: '10px 30px',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  },
  DiSvgContainer: {
    width: '100%',
    '& img': { width: '10%' },
  },
})

const Header = () => {
  const styles = useStyles();

  return (
    <div className={styles.header}>
      <DiSvg styles={styles.DiSvgContainer} />
    </div>
  )
}

export default Header;
