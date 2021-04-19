import {makeStyles} from "@material-ui/core";


const useStyles = makeStyles({
  container: {
    width: '40%',
    fontFamily: 'Poppins, sans-serif',
    '& h6': {
      fontFamily: 'Poppins, sans-serif',
      color: '#5926EB',
    },
  },
  titlesContainer: {
    marginBottom: '20px',
    '& > h4': {
      color: '#5926EB',
      fontWeight: 500,
    },
  },
  iconContainer: {
    color: '#5926EB',
    '& svg': {
      marginTop: '5px',
    },
  },
  searchExplanation: {
    gap: '10px',
    display: 'flex',
    background: 'rgba(35%, 15%, 92%, 5%)',
    borderRadius: '5px',
    padding: '30px 20px'
  }
});

export default useStyles;
