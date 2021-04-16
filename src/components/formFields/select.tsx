import React from 'react';
import { useField } from 'formik';
import {Select, FormControl, InputLabel, FormHelperText} from '@material-ui/core';

const MySelect = (props: any) => {
  const { errorText, required, size, className, name, label, ...rest } = props;
  const [field, meta] = useField(props);
  const {touched, error} = meta;

  function _renderHelperText() {
    if (touched && error) {
      return <FormHelperText error={true}>{error}</FormHelperText>;
    }
  }


  return (
    <FormControl
      size={size}
      variant={'outlined'}
      required={required}
      className={className}
      style={{background: '#fff'}}
    >
      <InputLabel
        style={{background: '#fff'}}
        error={!!(touched && error)}
        htmlFor={name}
      >{label}</InputLabel>
      <Select
        {...field}
        name={name}
        error={!!(meta.touched && meta.error)}
        native
      >
        <option aria-label="None" value="" />
        <option value={10}>Ten</option>
        <option value={20}>Twenty</option>
        <option value={30}>Thirty</option>
      </Select>
      {_renderHelperText()}
    </FormControl>
  );
}

export default MySelect;
