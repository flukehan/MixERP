CREATE FUNCTION core.get_root_parent_menu_id(text)
RETURNS integer
AS
$$
    DECLARE retVal integer;
BEGIN
    WITH RECURSIVE find_parent(menu_id_group, parent, parent_menu_id, recentness) AS
    (
            SELECT menu_id, menu_id, parent_menu_id, 0
            FROM core.menus
            WHERE url=$1
            UNION ALL
            SELECT fp.menu_id_group, i.menu_id, i.parent_menu_id, fp.recentness + 1
            FROM core.menus i
            JOIN find_parent fp ON i.menu_id = fp.parent_menu_id
    )

        SELECT parent INTO retVal
        FROM find_parent q 
        JOIN
        (
                SELECT menu_id_group, MAX(recentness) AS answer
                FROM find_parent
                GROUP BY menu_id_group 
        ) AS ans ON q.menu_id_group = ans.menu_id_group AND q.recentness = ans.answer 
        ORDER BY q.menu_id_group;

    RETURN retVal;
END
$$
LANGUAGE plpgsql;
