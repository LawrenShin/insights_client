import {makeStyles} from "@material-ui/core";
import {blue, darkBlueBack, loaderContainer, main} from "../colorConstants";


export const useStyles = makeStyles({
  container: {
    width: '100%',
    fontFamily: 'Poppins, sans-serif',
    '& h6': {
      fontFamily: 'Poppins, sans-serif',
      color: main,
    },
  },
  titlesContainer: {
    marginBottom: '20px',
    '& > h4': {
      color: main,
      fontWeight: 500,
    },
  },
  iconContainer: {
    color: main,
    '& svg': {
      marginTop: '5px',
    },
  },
  searchExplanation: {
    gap: '10px',
    display: 'flex',
    background: darkBlueBack,
    borderRadius: '5px',
    padding: '30px 20px'
  }
});

export const useIndustruOptionStyles = makeStyles({
  ...loaderContainer,
  root: {
    '& :hover': {
      cursor: 'pointer',
    }
  },
  list: {
    fontSize: '14px',
    display: 'flex',
    flexDirection: 'column',
    flexWrap: 'wrap',
    height: '900px',
    '& > div': {
      borderRight: `1px solid ${darkBlueBack}`,
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
    border: `1px solid ${darkBlueBack}`,
    borderRadius: '5px',
    padding: '10px 20px',
  },
  selected: {
    background: blue,
  }
});

export const useContryOptionsStyles = makeStyles({
  ...loaderContainer,
  searchCriteriasContainer: {
    gap: '10px',
    '& > div': {
      padding: '10px',
      background: 'white',
      borderRadius: '5px',
      border: `1px solid ${darkBlueBack}`,
      minHeight: '60vh',
      maxWidth: '200px',
    }
  },
  regionsContainer: {
    fontSize: '.8em',
    '& > div': {padding: '0'}
  },
  colTitle: {},
  colSubTitle: {
    fontSize: '.8em',
    color: blue,
  },
  line: {
    background: `${darkBlueBack}`,
    width: 'calc(100% + 20px)',
    marginLeft: '-10px',
    height: '1px',
  },
  arrowContainer: {
    display: 'flex',
    flexGrow: 1,
    justifyContent: 'flex-end',
  },
});

export default useStyles;
