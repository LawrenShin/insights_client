import {makeStyles} from "@material-ui/core";

export default makeStyles({
  inputs: {
    display: 'flex',
    flexDirection: 'column',
    gap: '20px',
  },
  button: {
    borderRadius: '30px',
    '&:hover': {
      color: '#5926EB',
      background: '#fff',
    },
  },
  textAlignRight: { textAlign: 'right', },
  fullWidth: { width: '100%' },
  whiteBack: {
    background: '#fff',
    color: '#5926EB',
    border: '1px solid #5926EB',
    '&:hover': {
      background: '#5926EB',
      color: 'white',
    }
  },
  loaderContainer: {
    display: 'flex',
    justifyContent: 'center',
  }
});