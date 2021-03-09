import {makeStyles} from "@material-ui/core";


export default makeStyles({
  root: {
    background: '#E5E5E5',
    height: '100vh',
    display: 'flex',
    flexDirection: 'column',
  },
  searchWrapper: {
    flexGrow: 1,
    display: 'flex',
    flexDirection: 'column',
    justifyContent: 'space-around',
    padding: '30px'
  },
  searchContainer: {
    background: '#E0E9FF',
    minHeight: '25%',
    maxHeight: '25%',
  },
  searchSettingsContainer: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    background: '#fff',
    minHeight: '70%',
  },
});



