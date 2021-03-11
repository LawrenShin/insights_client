import {makeStyles} from "@material-ui/core";

const purp = '#5926EB';

export default makeStyles({
  root: {

  },
  searchWrapper: {
    flexGrow: 1,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-around',
    padding: '30px',
    '& > span': {
      color: purp,
      marginBottom: '20px',
    },
    '& > h5': {
      font: 'Poppins',
      fontWeight : 'bold',
      marginBottom: '20px',
    }
  },
  content: {
    display: 'flex',
    flexDirection: 'column',
    background: '#fff',
    boxShadow: '0px 2px 20px rgba(0, 0, 0, 0.05)',
  }
});



