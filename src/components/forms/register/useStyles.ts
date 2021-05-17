import {makeStyles} from "@material-ui/core";
import {loaderContainer} from "../../colorConstants";

const useStyles = makeStyles({
  ...loaderContainer,
  DiSvgContainer: {
    width: '100%',
    '& img': { width: '30%' },
  },
  buttonContainer: {
    display: 'flex',
    marginTop: '35px',
    justifyContent: 'space-between',
    '& > button': {
      width: 'auto',
    }
  },
  nextButton: {
    borderRadius: '30px',
    fontFamily: 'Poppins, sans-serif',
    fontWeight: 'bold',
    paddingRight: '30px',
    paddingLeft: '30px',
  },
  backButton: {
    color: 'rgba(0,0,0, 0.5)',
    lineHeight: 2,
    '&:hover': {
      cursor: 'pointer',
    }
  },
});

export default useStyles;
