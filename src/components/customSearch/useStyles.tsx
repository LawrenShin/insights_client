import {makeStyles} from "@material-ui/core";
import {loaderContainer, purpBack} from "../colorConstants";


const useStyles = makeStyles({
  container: {
    width: '100%',
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

export const useIndustruOptionStyles = makeStyles({
  ...loaderContainer,
  root: {
  },
  list: {
    fontSize: '14px',
    display: 'flex',
    flexDirection: 'column',
    flexWrap: 'wrap',
    height: '900px',
    '& > div': {
      borderRight: `1px solid ${purpBack}`,
      width: '25%',
    },
  },
  marginLeft15: {paddingLeft: '15px'},
  parent: {
    '& span': {
      color: '#524D74',
      fontWeight: 'bold',
    },
  },
  searchTitle: {
    border: `1px solid ${purpBack}`,
    borderRadius: '5px',
    padding: '10px 20px',
  }
});

export default useStyles;
