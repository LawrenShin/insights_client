import {makeStyles} from "@material-ui/core";

const useStyles = makeStyles({
  title: {
    fontWeight: 'bold',
    fontFamily: 'Poppins, sans-serif',
    marginBottom: '1em',
  },
  fullWidth: {
    width: '100%',
  },
  colorPurp: {
    color: '#5926EB',
  },
  welcome: {
    marginBottom: '10px',
  },
  button: {
    borderRadius: '30px',
    margin: '10px 0',
    '&:hover': {
      color: '#5926EB',
      background: '#fff',
    },
  },
  buttonsContainer: {
    textAlign: 'center',
  },
  resend: {
    fontWeight: 'bold',
    fontFamily: 'Poppins, sans-serif',
  },
  backToSignIn: {
    fontSize: '0.8em',
    fontWeight: 'bold',
    color: '#5926EB',
    background: '#fff',
    '&:hover': {
      cursor: 'pointer',
    },
    }
});

export default useStyles;
