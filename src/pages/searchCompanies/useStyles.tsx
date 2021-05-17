import {makeStyles} from "@material-ui/core";


export default makeStyles({
  root: {
    background: '#E5E5E5',
    minHeight: '100vh',
    display: 'flex',
    flexDirection: 'column',
  },
  searchWrapper: {
    display: 'flex',
    flexDirection: 'column',
    padding: '30px',
    gap: '15px',
  },
  searchContainer: {
    background: '#E0E9FF',
  },
  searchSettingsContainer: {
    minHeight: '60vh',
    justifyContent: 'center',
    alignItems: 'center',
    background: '#fff',
    borderRadius: '5px',
  },
});



