import {createStyles, makeStyles, Theme} from "@material-ui/core";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      gap: '15px',
    },
    paper: {
      padding: theme.spacing(2),
      color: theme.palette.text.secondary,
      background: '#fff',
      border: '1px solid #ccc',
      borderRadius: '5px'
    },
    title: {
      marginBottom: '20px',
      '& span': {
        fontWeight: 'bold',
        fontSize: '1.1em',
        color: 'black',
      }
    }
  }),
);


export default useStyles;